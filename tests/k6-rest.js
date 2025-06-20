// k6 run k6-rest.js

import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 50, // Sanal kullanıcı sayısı
    duration: '10s',
};

export default function () {
    let res = http.get('http://localhost:5000/api/books');
    check(res, { 'status was 200': (r) => r.status == 200 });
    sleep(0.1);
}
