
$BasePath = Get-Location 

$BrokerPath  = Join-Path -Path $BasePath -ChildPath "CoinProcessor.Broker\bin\Debug\netcoreapp2.0\win10-x64\publish\CoinProcessor.Broker.exe"

$SubscriberPath  = Join-Path -Path $BasePath -ChildPath "CoinProcessor.Subscriber\bin\Debug\netcoreapp2.0\win10-x64\publish\CoinProcessor.Subscriber.exe"

$PublisherPath  = Join-Path -Path $BasePath -ChildPath "CoinProcessor.Publisher\bin\Debug\netcoreapp2.0\win10-x64\publish\CoinProcessor.Publisher.exe"

start-process powershell -FilePath "$BrokerPath" 

start-process -FilePath "$SubscriberPath" -ArgumentList "*.*.biggerThan10"

start-process -FilePath "$SubscriberPath" -ArgumentList "*.ether"

start-process -FilePath "$SubscriberPath" -ArgumentList "*.bitcoin"

start-process -FilePath "$SubscriberPath" -ArgumentList "*.*.*.dateKey"

start-process powershell -FilePath "$PublisherPath" 
