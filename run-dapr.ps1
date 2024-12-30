# Run ClientMS Dapr instance
Start-Process "dapr" -ArgumentList "run --app-id clientms --app-port 8000 --dapr-http-port 3500 -- python .\clientmanagement-ms\manage.py runserver"

# Run OrderMS Dapr instance
Start-Process "dapr" -ArgumentList "run --app-id orderms --app-port 5086 --app-protocol http --resources-path ./Components --dapr-http-port 3501 -- dotnet run --project .\OrderMS\OrderMS.csproj"
