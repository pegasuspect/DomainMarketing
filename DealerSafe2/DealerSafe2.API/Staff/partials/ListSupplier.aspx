﻿<list-header title="Suppliers"></list-header>

<form id="form" class="form-inline" role="form" autocomplete="off">
    <input-select horizontal="1" label="API" name="ApiId" model="selectedApiId" options="i.Id as i.Name for i in Apis"></input-select>
    <input type="button" value="Filtrele" ng-click="search()" />
</form>

<pagination></pagination>

<table class="table table-striped table-bordered table-hover">
    <thead>
        <tr>
            <th></th>
            <th column-header="Name" field="Name"></th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <tr ng-repeat="entity in list" ng-class="{deleted:entity.IsDeleted}">
            <td indexer></td>
            <td link-to-view>{{entity.Name}}</td>
            <td operations></td>
        </tr>
    </tbody>
</table>

<list-footer></list-footer>

