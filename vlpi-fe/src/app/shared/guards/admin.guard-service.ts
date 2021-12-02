import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { UserSyncService } from "../services/user.sync.service";

@Injectable({providedIn: 'root'})
export class AdminGuardService implements CanActivate {
  constructor(public userService: UserSyncService, public router: Router) {}
  canActivate(): boolean {
    if (!this.userService.isUserAdmin()) {
      this.router.navigate(['requirements']);
      return false;
    }
    return true;
  }
}