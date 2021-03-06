param defaultResourceName string
param location string
param storageAccountTables array
param containerVersion string

param containerAppEnvironmentResourceGroupName string
param containerAppEnvironmentResourceName string

resource containerAppEnvironments 'Microsoft.App/managedEnvironments@2022-03-01' existing = {
  name: containerAppEnvironmentResourceName
  scope: resourceGroup(containerAppEnvironmentResourceGroupName)
}

resource storageAccount 'Microsoft.Storage/storageAccounts@2021-09-01' = {
  name: uniqueString(defaultResourceName)
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
}
resource storageAccountTableService 'Microsoft.Storage/storageAccounts/tableServices@2021-09-01' = {
  name: 'default'
  parent: storageAccount
}
resource storageAccountTable 'Microsoft.Storage/storageAccounts/tableServices/tables@2021-09-01' = [for table in storageAccountTables: {
  name: table
  parent: storageAccountTableService
}]
resource redisCache 'Microsoft.Cache/redis@2021-06-01' = {
  name: '${defaultResourceName}-cache'
  location: location
  properties: {
    sku: {
      name: 'Standard'
      capacity: 1
      family: 'C'
    }
    enableNonSslPort: false
    publicNetworkAccess: 'Enabled'
  }
}

resource apiContainerApp 'Microsoft.App/containerApps@2022-03-01' = {
  name: '${defaultResourceName}-cnt-api'
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    managedEnvironmentId: containerAppEnvironments.id
    configuration: {
      activeRevisionsMode: 'Single'
      secrets: [
        {
          name: 'storage-account-secret'
          value: listKeys(storageAccount.id, storageAccount.apiVersion).keys[0].value
        }
        {
          name: 'redis-cache-secret'
          value: listKeys(redisCache.id, redisCache.apiVersion).primaryKey
        }
      ]
      ingress: {
        external: true
        targetPort: 80
        transport: 'auto'
        allowInsecure: false
        traffic: [
          {
            weight: 100
            latestRevision: true
          }
        ]
      }
    }
    template: {
      containers: [
        {
          image: 'docker.io/nikneem/pollstar-api:${containerVersion}'
          name: 'pollstar-api'
          resources: {
            cpu: json('0.25')
            memory: '0.5Gi'
          }
          env: [
            {
              name: 'Cache_Secret'
              secretRef: 'redis-cache-secret'
            }
            {
              name: 'Cache_Endpoint'
              value: '${redisCache.name}.redis.cache.windows.net'
            }
            {
              name: 'Azure_StorageAccount'
              value: storageAccount.name
            }
            {
              name: 'Azure_StorageKey'
              secretRef: 'storage-account-secret'
            }
          ]
        }
      ]
      scale: {
        minReplicas: 0
        maxReplicas: 10
      }
    }
  }
}
