/*
    TOTAL RESULTS

    checks_total.......................: 4800    474.225873/s
    checks_succeeded...................: 100.00% 4800 out of 4800
    checks_failed......................: 0.00%   0 out of 4800

    ✓ status was 200

    HTTP
    http_req_duration.......................................................: avg=109.46ms min=100.12ms med=104.73ms max=308.31ms p(90)=109.83ms p(95)=112.81ms
      { expected_response:true }............................................: avg=109.46ms min=100.12ms med=104.73ms max=308.31ms p(90)=109.83ms p(95)=112.81ms
    http_req_failed.........................................................: 0.00%  0 out of 4800
    http_reqs...............................................................: 4800   474.225873/s

    EXECUTION
    iteration_duration......................................................: avg=210.49ms min=200.22ms med=205.77ms max=412.31ms p(90)=211.23ms p(95)=214.31ms
    iterations..............................................................: 4800   474.225873/s
    vus.....................................................................: 100    min=100       max=100
    vus_max.................................................................: 100    min=100       max=100

    NETWORK
    data_received...........................................................: 1.4 MB 143 kB/s
    data_sent...............................................................: 466 kB 46 kB/s
*/

import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 100,
    duration: '10s',
};

export default function () {
    let res = http.get('http://localhost:5052/api/authors/with-books-http');
    check(res, { 'status was 200': (r) => r.status == 200 });
    sleep(0.1);
}
