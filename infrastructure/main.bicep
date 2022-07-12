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

var defaultResourceName = toLower('${systemName}-${environmentName}-${locationAbbreviation}')
var resourceGroupName = toLower(defaultResourceName)

var storageAccountTables = [
  'users'
  'polls'
  'votes'
]

resource targetResourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: resourceGroupName
  location: location
}

module resourcesModule 'resources.bicep' = {
  name: 'ResourceModule'
  scope: targetResourceGroup
  params: {
    defaultResourceName: defaultResourceName
    location: location
    storageAccountTables: storageAccountTables
    containerVersion: containerVersion
  }
}
