param location string = 'westeurope'

resource ddosProtection 'Microsoft.Network/ddosProtectionPlans@2020-11-01' = {
  name: 'myDdosProtection'
  location: location
  properties: {}
}

resource publicLoadBalancer 'Microsoft.Network/loadBalancers@2020-11-01' = {
  name: 'myPublicLoadBalancer'
  location: location
  properties: {
    frontendIPConfigurations: [
      {
        name: 'myFrontEndIpConfig'
        properties: {
          publicIPAddress: {
            id: resourceId('Microsoft.Network/publicIPAddresses', 'myPublicIP')
          }
        }
      }
    ]
  }
}

resource firewall 'Microsoft.Network/azureFirewalls@2020-11-01' = {
  name: 'myFirewall'
  location: location
  properties: {
    sku: {
      name: 'AZFW_VNet'
      tier: 'Standard'
    }
  }
}

resource vnet 'Microsoft.Network/virtualNetworks@2020-11-01' = {
  name: 'myVnet'
  location: location
  properties: {
    addressSpace: {
      addressPrefixes: [
        '10.0.0.0/16'
      ]
    }
    subnets: [
      {
        name: 'default'
        properties: {
          addressPrefix: '10.0.0.0/24'
          networkSecurityGroup: {
            id: resourceId('Microsoft.Network/networkSecurityGroups', 'myNsg')
          }
        }
      }
    ]
  }
}

resource appService 'Microsoft.Web/serverfarms@2020-12-01' = {
  name: 'myAppServicePlan'
  location: location
  sku: {
    name: 'F1'
    tier: 'Free'
  }
}

resource storageAccount 'Microsoft.Storage/storageAccounts@2021-01-01' = {
  name: 'mystorageaccount'
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
}

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2019-05-01' = {
  name: 'myContainerRegistry'
  location: location
  sku: {
    name: 'Basic'
  }
  properties: {
    adminUserEnabled: true
  }
}

resource backup 'Microsoft.RecoveryServices/vaults@2021-01-01' = {
  name: 'myBackupVault'
  location: location
  properties: {
    sku: {
      name: 'Standard'
    }
  }
}
