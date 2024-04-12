Connect-AzAccount

$SubscriptionName="Azure Subscription 1"
$Subcription=Get-AzSubscription -SubscriptionName $SubscriptionName
Set-AzContext -SubscriptionObject $Subcription

$StorageAccountName="appstore500505"

$StorageAccountKey=(Get-AzStorageAccountKey -ResourceGroupName $ResourceGroupName `
-AccountName $StorageAccountName) | Where-Object {$_.KeyName -eq "key1"}

$StorageAccountKeyValue=$StorageAccountKey.Value

$SecretValue = ConvertTo-SecureString $StorageAccountKeyValue -AsPlainText -Force

$KeyVaultName="appvault67768"
Set-AzKeyVaultSecret -VaultName $KeyVaultName -Name $StorageAccountName `
-SecretValue $SecretValue
