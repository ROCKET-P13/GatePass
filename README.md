Build Steps:

1. `cd` into the .NET project (MotoPassAPI) and run:
```
dotnet lambda package --configuration Release --framework net8.0 --output-package bin/Release/net8.0/MotoPassAPI.zip
```

2. `cd` into the AWS CDK infrastrucutre (MotoPassInfrastructure) project and run:
```
cdk bootstrap
```

3. In the AWS CDK infrastructure project run:
```
cdk deploy
```


Deleting Stack:

1. `cd` into the AWS CDK infrastructure project and run:
```
cdk destroy MotoPassInfrastructureStack
```


For all `cdk` commands, if you do not have a default AWS profile, specify the profile as such:
```
cdk bootstrap --profile MyUser-Profile
```