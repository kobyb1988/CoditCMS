@using $rootnamespace$
@model DB.Entities.IEntity

<a class="list-action" href="@Url.Action(Constants.EditView, new { id = Model.Id })"><img src="/Areas/Admin/Images/Default/edit.png" alt="Редактировать" /></a>
