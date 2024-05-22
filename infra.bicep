// Parametri
param location string = resourceGroup().location
param serverName string = 'mySqlServer${uniqueString(resourceGroup().id)}'
param databaseName string = 'myDatabase'
param adminUsername string = 'sqlAdmin'
param adminPassword string = 'P@ssw0rd!23' // Nota: in un contesto reale, utilizzare un meccanismo sicuro per gestire le password
param appServicePlanName string = 'myAppServicePlan'
param webAppName string = 'myWebApiApp${uniqueString(resourceGroup().id)}'

// Risorsa SQL Server
resource sqlServer 'Microsoft.Sql/servers@2022-02-01-preview' = {
  name: serverName
  location: location
  properties: {
    administratorLogin: adminUsername
    administratorLoginPassword: adminPassword
  }
  sku: {
    name: 'GP_Gen5_2'
    tier: 'GeneralPurpose'
    family: 'Gen5'
    capacity: 2
  }
}

// Risorsa Database SQL
resource sqlDatabase 'Microsoft.Sql/servers/databases@2022-02-01-preview' = {
  name: '${serverName}/${databaseName}'
  location: location
  properties: {
    readScale: 'Disabled'
  }
  sku: {
    name: 'S0'
    tier: 'Standard'
  }
  dependsOn: [
    sqlServer
  ]
}

// Piano di App Service
resource appServicePlan 'Microsoft.Web/serverfarms@2021-02-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'P1v2'
    tier: 'PremiumV2'
    capacity: 1
  }
}

// App Service per la Web API
resource webApp 'Microsoft.Web/sites@2021-02-01' = {
  name: webAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      appSettings: [
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: 'Production'
        }
        {
          name: 'ConnectionStrings__DefaultConnection'
          value: 'Server=tcp:${sqlServer.name}.database.windows.net,1433;Initial Catalog=${sqlDatabase.name};Persist Security Info=False;User ID=${adminUsername};Password=${adminPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
        }
      ]
    }
  }
  dependsOn: [
    appServicePlan
    sqlDatabase
  ]
}
