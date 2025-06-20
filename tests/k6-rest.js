/*
    TOTAL RESULTS

    checks_total.......................: 4903    481.041585/s
    checks_succeeded...................: 100.00% 4903 out of 4903
    checks_failed......................: 0.00%   0 out of 4903

    HTTP
    http_req_duration.......................................................: avg=103.65ms min=99.39ms  med=102.52ms max=142.87ms p(90)=106.06ms p(95)=108.2ms
      { expected_response:true }............................................: avg=103.65ms min=99.39ms  med=102.52ms max=142.87ms p(90)=106.06ms p(95)=108.2ms
    http_req_failed.........................................................: 0.00%  0 out of 4903
    http_reqs...............................................................: 4903   481.041585/s

    EXECUTION
    iteration_duration......................................................: avg=204.91ms min=199.62ms med=203.7ms  max=243.77ms p(90)=208.41ms p(95)=210.6ms
    iterations..............................................................: 4903   481.041585/s
    vus.....................................................................: 100    min=100       max=100
    vus_max.................................................................: 100    min=100       max=100

    NETWORK
    data_received...........................................................: 1.0 MB 103 kB/s
    data_sent...............................................................: 387 kB 38 kB/s
*/

import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 100,
    duration: '10s',
};

export default function () {
    let res = http.get('http://localhost:5051/api/books');
    check(res, { 'status was 200': (r) => r.status == 200 });
    sleep(0.1);
}
