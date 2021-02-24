echo off

docker build -t test-web-api-application:1.0.18 -f Dockerfile .. || goto :error
docker tag test-web-api-application:1.0.18 codefuller/test-web-api-application:1.0.18 || goto :error
docker push codefuller/test-web-api-application:1.0.18 || goto :error

kubectl apply -f TestWebApiApplication.yaml --namespace=test || goto :error
rem kubectl rollout restart deployment.apps/test-web-api-application || goto :error

echo [92mDeployed successfully![0m
goto :EOF

:error

set exit_code=%errorlevel%
if "%exit_code%"=="0" (
    set exit_code=1
)

echo [91mDeployment has FAILED with the error #%exit_code%[0m
exit /b %exit_code%
