import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../core/services/admin';
import { DashboardStats } from '../../core/models/dashboard-stats.model';
import { Router, RouterModule } from "@angular/router";

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.html',
  imports: [RouterModule]
})
export class AdminDashboardComponent implements OnInit {

  stats: DashboardStats | null = null;
  loading = true;

  constructor(
  private adminService: AdminService,
  private router: Router
){}

  ngOnInit(): void {
    this.loadDashboard();
  }

  loadDashboard() {
    this.adminService.getDashboardStats().subscribe({
      next: (res) => {
        this.stats = res;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      }
    });
  }
  logout() {
  localStorage.removeItem('token');
  this.router.navigate(['/login']);
}
}