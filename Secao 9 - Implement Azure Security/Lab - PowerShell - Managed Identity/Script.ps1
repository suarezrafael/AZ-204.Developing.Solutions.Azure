Connect-AzAccount

$ResourceGroupName="app-grp" 
$VmName="appvm"

$Vm=Get-AzVM -ResourceGroupName $ResourceGroupName -Name $VmName
Update-AzVM -ResourceGroupName $ResourceGroupName -VM $Vm -IdentityType SystemAssigned