# EventAggregatorRx
Simple EventAggregator class using Rx.

## Download
### Via NuGet
To install library by using [Nuget package](https://www.nuget.org/packages/EventAggregatorRx) manager execute next command:

```
Install-Package EventAggregatorRx -Version 1.0.0.1
```

## Example
Design events using IEvent interface.

```csharp
public class ItemAdded : IEvent 
{
  public Guid ItemId {get; set;}
}
```

Next step, implement your events in the viewModel and subscribe.

```csharp
public class BasketViewModel : IEventHandler<ItemAdded>
{
  ...
  public BasketViewModel(IEventAggregator event) 
  {
    eventAggregator.Subscribe(this); //subscribing viewmodel
  }
  
  public void Handle(ItemAdded @event)
  {
      //TODO: Any action
  }
}
```

Next step: Publish the event in another viewmodel.

```csharp
public class CatalogViewModel
{
    ...
    public CatalogViewModel(IEventAggregator eventAggregator)
    {
        eventAggregator.Publish(new ItemAdded {ItemId = Guid.NewGuid()});
    }
}
```
