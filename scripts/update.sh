curl --location --request PATCH 'http://localhost:5000/customer/' \
--header 'Content-Type: application/json' \
--data-raw '{
  "Id": "$1",
  "firstName": "$2",
  "lastName": "$3",
  "age": $4
}'