/*
    TOTAL RESULTS

    checks_total.......................: 4800    477.8365/s
    checks_succeeded...................: 100.00% 4800 out of 4800
    checks_failed......................: 0.00%   0 out of 4800

    ✓ status was 200

    HTTP
    http_req_duration.......................................................: avg=108.36ms min=99.35ms med=105.66ms max=216.94ms p(90)=111.03ms p(95)=112.96ms
      { expected_response:true }............................................: avg=108.36ms min=99.35ms med=105.66ms max=216.94ms p(90)=111.03ms p(95)=112.96ms
    http_req_failed.........................................................: 0.00%  0 out of 4800
    http_reqs...............................................................: 4800   477.8365/s

    EXECUTION
    iteration_duration......................................................: avg=209.21ms min=199.6ms med=206.37ms max=321.66ms p(90)=212.04ms p(95)=214.34ms
    iterations..............................................................: 4800   477.8365/s
    vus.....................................................................: 100    min=100       max=100
    vus_max.................................................................: 100    min=100       max=100

    NETWORK
    data_received...........................................................: 1.4 MB 144 kB/s
    data_sent...............................................................: 466 kB 46 kB/s
*/

import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 100,
    duration: '10s',
};

export default function () {
    let res = http.get('http://localhost:5052/api/authors/with-books-grpc');
    check(res, { 'status was 200': (r) => r.status == 200 });
    sleep(0.1);
}
