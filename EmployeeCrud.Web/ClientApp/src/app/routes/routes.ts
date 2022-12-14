import { Routes } from "@angular/router";
import { HomeComponent } from "../components/home/home.component";
import { LoginComponent } from "../components/login/login.component";
import { AuthGuard } from "../security/auth.guard";

export const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: "", component: HomeComponent, pathMatch: "full", canActivate: [AuthGuard] },
];
