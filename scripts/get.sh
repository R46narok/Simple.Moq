echo $1
curl --location --request GET 'http://localhost:5000/customer/{$1}'