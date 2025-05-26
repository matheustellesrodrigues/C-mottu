#!/bin/bash

# login
az login

# variáveis
RG=ApiOracleGroup
ACR_NAME=apioracleacr
APP_NAME=apioracleapp

# criar grupo
az group create --name $RG --location eastus

# criar registro docker
az acr create --name $ACR_NAME --sku Basic --resource-group $RG --admin-enabled true
az acr login --name $ACR_NAME

# build da imagem local
docker build -t $ACR_NAME.azurecr.io/apioracle .

# push
docker push $ACR_NAME.azurecr.io/apioracle

# pegar credenciais
USERNAME=$(az acr credential show --name $ACR_NAME --query username -o tsv)
PASSWORD=$(az acr credential show --name $ACR_NAME --query passwords[0].value -o tsv)

# criar container instance
az container create \
  --resource-group $RG \
  --name $APP_NAME \
  --image $ACR_NAME.azurecr.io/apioracle \
  --dns-name-label apioracle \
  --ports 80 \
  --registry-login-server $ACR_NAME.azurecr.io \
  --registry-username $USERNAME \
  --registry-password $PASSWORD
