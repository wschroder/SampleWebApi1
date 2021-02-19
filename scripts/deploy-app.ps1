$gitRepo = "https://github.com/wschroder/SampleWebApi1.git"
$webAppName = "SampleWebApi$(Get-Random)"
$location = "East US"
$env = "dev"
$rgName = "$($env)-gfc-permitter"
$servicePlan = "$($env)-gfc-fire-permitter-plan"
$apiName = "$($env)-gfc-fire-permitter-api"
$webName = "$($env)-gfc-fire-permitter-web"

# (env)-(company)-(domain)-(item)-(type)

New-AzResourceGroup -Name $rgName -Location $location

New-AzAppServicePlan -Name $servicePlan -Location $location -ResourceGroupName $rgName -Tier Free

New-AzWebApp -Name $webName -Location $location -AppServicePlan $servicePlan  -ResourceGroupName $rgName

New-AzWebApp -Name $apiName -Location $location -AppServicePlan $servicePlan  -ResourceGroupName $rgName

$propertiesObject = @{
    repoUrl = $gitRepo;
    branch = "master";
    isManualIntegration = true
}

Set-AzResource -Properties $propertiesObject -ResourceGroupName $rgName -ResourceType Microsoft.Web/sites/sourcecontrols -ResourceName $webAppName/web -ApiVersion 2015-08-01 -Force