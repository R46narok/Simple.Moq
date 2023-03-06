curl --location --request POST 'http://localhost:5000/customer/' \
--header 'Content-Type: application/json' \
--data-raw '{
  "firstName": "$1",
  "lastName": "$2",
  "age": $3
}'