import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../core/services/users';
import { UserInfo } from '../../core/models/user.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-list',
  imports: [FormsModule, CommonModule],
  templateUrl: './user-list.html'
})
export class UserListComponent implements OnInit {

  users: UserInfo[] = [];

  constructor(private userService: UsersService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getAllUsersInfo().subscribe(res => {
      this.users = res;
    });
  }

  deleteUser(id: number) {
    if (confirm('Are you sure?')) {
      this.userService.deleteUserInfo(id).subscribe(() => {
        this.loadUsers();
      });
    }
  }

  toggleLock(user: UserInfo) {
    this.userService.lockUnlockUser(user.id, !user.isLocked)
      .subscribe(() => this.loadUsers());
  }

  changeRole(id: number, role: string) {
    this.userService.updateRole(id, role).subscribe(() => {
      this.loadUsers();
    });
  }
}