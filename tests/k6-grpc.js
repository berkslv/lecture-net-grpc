/*
    TOTAL RESULTS

    checks_total.......................: 4823    472.693405/s
    checks_succeeded...................: 100.00% 4823 out of 4823
    checks_failed......................: 0.00%   0 out of 4823

    EXECUTION
    iteration_duration.....................: avg=208.13ms min=200.14ms med=206.49ms max=244.75ms p(90)=215.05ms p(95)=220.15ms
    iterations.............................: 4823   472.693405/s
    vus....................................: 100    min=100      max=100
    vus_max................................: 100    min=100      max=100

    NETWORK
    data_received..........................: 922 kB 90 kB/s
    data_sent..............................: 1.2 MB 117 kB/s

    GRPC
    grpc_req_duration......................: avg=102.63ms min=99.37ms  med=101.9ms  max=118.76ms p(90)=105.46ms p(95)=107.57ms
*/

import grpc from 'k6/net/grpc';
import { check, sleep } from 'k6';

const client = new grpc.Client();
// Load proto file from the tests directory
client.load(['.'], 'book.proto');

export let options = {
    vus: 100,
    duration: '10s',
};

export default function () {
    client.connect('localhost:5851', {
        plaintext: true // TLS kapalıysa
    });

    // Use the correct service and method name
    const response = client.invoke('books.BookService/GetBooks', {});
    check(response, {
        'status is OK': (r) => r && r.status === grpc.StatusOK,
    });

    client.close();
    sleep(0.1);
}
