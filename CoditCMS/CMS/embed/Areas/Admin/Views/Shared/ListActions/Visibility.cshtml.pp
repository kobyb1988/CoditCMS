@using $rootnamespace$
@using DB.Entities
@model DB.Entities.IEntity

@if (((IVisibleEntity)Model).Visibility)
{
    <a class="list-action" href="@Url.Action(Constants.SetVisibilityAction, new {id = Model.Id, visibilityToSet = false })"><img src="/Areas/Admin/Images/Default/VisibilityOn.png" alt="Выключить" /></a>
}
else
{
    <a class="list-action" href="@Url.Action(Constants.SetVisibilityAction, new { id = Model.Id, visibilityToSet = true })"><img src="/Areas/Admin/Images/Default/VisibilityOff.png" alt="Включить" /></a>
}

