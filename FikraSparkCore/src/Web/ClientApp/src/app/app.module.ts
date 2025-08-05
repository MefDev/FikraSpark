import { BrowserModule } from '@angular/platform-browser';
import { APP_ID, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { IdeaComponent } from './Idea/Idea.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './Auth/login/login.component';
import { RegisterComponent } from './Auth/register/register.component';
import { ToastComponent } from './Shared/toast/toast.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        IdeaComponent,
        RegisterComponent,
        LoginComponent,
        ToastComponent
    ],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: 'register', component: RegisterComponent },
            { path: 'login', component: LoginComponent },
            { path: 'idea', component: IdeaComponent }

        ]),
        BrowserAnimationsModule,
        DragDropModule,
        CommonModule, 
        ReactiveFormsModule, 
    ],
    providers: [
        { provide: APP_ID, useValue: 'ng-cli-universal' },
        { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
        provideHttpClient(withInterceptorsFromDi())
    ]
})
export class AppModule { }
