// k6 run k6-grpcjs

import grpc from 'k6/net/grpc';
import { check, sleep } from 'k6';

const client = new grpc.Client();
client.load(['.'], 'book.proto');

export let options = {
    vus: 50,
    duration: '10s',
};

export default function () {
    client.connect('localhost:5001', {
        plaintext: true // TLS kapalıysa
    });

    const response = client.invoke('BookService.GetBooks', {});
    check(response, {
        'status is OK': (r) => r && r.status === grpc.StatusOK,
    });

    client.close();
    sleep(0.1);
}
