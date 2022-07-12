targetScope = 'subscription'

param systemName string

@allowed([
  'dev'
  'test'
  'prod'
])
param environmentName string
param location string = deployment().location
param locationAbbreviation string
param containerVersion string

var apiResourceGroupName = toLower('${systemName}-api-${environmentName}-${locationAbbreviation}')
var integrationResourceGroupName = toLower('${systemName}-integration-${environmentName}-${locationAbbreviation}')

var storageAccountTables = [
  'users'
  'polls'
  'votes'
]

resource apiResourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: apiResourceGroupName
  location: location
}
resource integrationResourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: integrationResourceGroupName
  location: location
}

module integrationModule 'integration.bicep' = {
  name: 'IntegrationModule'
  scope: integrationResourceGroup
  params: {
    defaultResourceName: toLower('${systemName}-int-${environmentName}-${locationAbbreviation}')
    location: location
  }
}

module resourcesModule 'resources.bicep' = {
  name: 'ResourceModule'
  scope: apiResourceGroup
  params: {
    defaultResourceName: toLower('${systemName}-api-${environmentName}-${locationAbbreviation}')
    location: location
    storageAccountTables: storageAccountTables
    containerVersion: containerVersion
    containerAppEnvironmentResourceGroupName: integrationResourceGroup.name
    containerAppEnvironmentResourceName: integrationModule.outputs.containerAppEnvironmentName
  }
}
