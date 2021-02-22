echo off

docker build -t test-grpc-caller:1.0.1 -f Dockerfile .. || goto :error
docker tag test-grpc-caller:1.0.1 codefuller/test-grpc-caller:1.0.1 || goto :error
docker push codefuller/test-grpc-caller:1.0.1 || goto :error

kubectl apply -f TestGrpcCaller.yaml --namespace=test || goto :error

echo [92mDeployed successfully![0m
goto :EOF

:error

set exit_code=%errorlevel%
if "%exit_code%"=="0" (
    set exit_code=1
)

echo [91mDeployment has FAILED with the error #%exit_code%[0m
exit /b %exit_code%
