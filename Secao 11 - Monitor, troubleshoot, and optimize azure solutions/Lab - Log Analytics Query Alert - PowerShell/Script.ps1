# Heartbeat | where TimeGenerated > ago(30m)

Connect-AzAccount

$LogQuery="Heartbeat | where TimeGenerated > ago(30m)"
$DataSourceId="/subscriptions/6912d7a0-bc28-459a-9407-33bbba641c07/resourceGroups/app-grp/providers/Microsoft.OperationalInsights/workspaces/appworkspace"

$RuleSource = New-AzScheduledQueryRuleSource -Query $LogQuery `
                  -DataSourceId $DataSourceId `
				  -QueryType "ResultCount"

$RuleSchedule=New-AzScheduledQueryRuleSchedule -FrequencyInMinutes 5 -TimeWindowInMinutes 5

$TriggerCondition=New-AzScheduledQueryRuleTriggerCondition -ThresholdOperator "GreaterThan" -Threshold 3

$ActionGroupId="/subscriptions/6912d7a0-bc28-459a-9407-33bbba641c07/resourcegroups/app-grp/providers/Microsoft.Insights/actiongroups/GroupA"
$ActionGroup=New-AzScheduledQueryRuleAznsActionGroup -ActionGroup `
@($ActionGroupId) -EmailSubject "Log Alert"

$AlertAction = New-AzScheduledQueryRuleAlertingAction -AznsAction $ActionGroup -Severity "1" `
-Trigger $TriggerCondition

$ResourceGroupName="app-grp"
New-AzScheduledQueryRule -ResourceGroupName $ResourceGroupName -Location "North Europe" `
-Action $AlertAction -Enable $true -Description "This is an alert based on Log Analytics" `
-Schedule $RuleSchedule -Source $RuleSource -Name "Log Analytics alert"