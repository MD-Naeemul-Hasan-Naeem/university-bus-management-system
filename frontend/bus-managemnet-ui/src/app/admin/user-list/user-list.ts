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

  showAddForm: boolean = false;

  newUser: any = {
    email: '',
    password: '',
    role: 'User'
  };

 

  constructor(private userService: UsersService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getAllUsersInfo().subscribe(res => {
      this.users = res;
    });
  }

   openAddForm() {
  this.showAddForm = true;
}

closeAddForm() {
  this.showAddForm = false;
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
  searchText: string = '';

filteredUsers(): UserInfo[] {
  if (!this.searchText) return this.users;

  return this.users.filter(u =>
    u.email.toLowerCase().includes(this.searchText.toLowerCase())
  );
}
addUser() {
  alert('Add User clicked (you can open modal here)');
}

createUser() {
   const payload = {
    ...this.newUser,
    fullName: this.newUser.fullName || '',
    phone: this.newUser.phone || '',
    department: this.newUser.department || '',
    studentId: null,
    employeeId: null
  };

  this.userService.createUser(payload).subscribe({
    next: () => {
      alert('User created successfully');
      this.closeAddForm();
      this.loadUsers();
    },
    error: (err) => {
      console.error(err);
    }
  });
}

}