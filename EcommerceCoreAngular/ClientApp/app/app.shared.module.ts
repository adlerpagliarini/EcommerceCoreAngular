import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser'; //
import { FormsModule, ReactiveFormsModule, NG_VALIDATORS, FormControl } from '@angular/forms'; //
//import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from '@angular/http'; //
//import { HttpModule } from '@angular/http';
import { requestOptionsProvider } from './services/default-request-options.service';
import { RouterModule } from '@angular/router';
import { FlashMessagesModule } from 'angular2-flash-messages';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';

import { CategoryComponent } from './components/category/category.component';
import { CategoryListComponent } from './components/category/category-list.component';



@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        FetchDataComponent,
        HomeComponent,
        CategoryComponent, CategoryListComponent //
    ],
    imports: [
        BrowserModule, //
        CommonModule,
        HttpModule,
        JsonpModule, //
        FormsModule,
        ReactiveFormsModule, //
        FlashMessagesModule, //
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'category', component: CategoryComponent },
            { path: 'category/:id', component: CategoryComponent },
            { path: 'category-list', component: CategoryListComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [requestOptionsProvider]
})
export class AppModuleShared {
}
