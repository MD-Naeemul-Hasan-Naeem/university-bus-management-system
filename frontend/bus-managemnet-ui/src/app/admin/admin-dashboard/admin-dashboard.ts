import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../core/services/admin';
import { DashboardStats } from '../../core/models/dashboard-stats.model';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.html',
})
export class AdminDashboardComponent implements OnInit {

  stats: DashboardStats | null = null;
  loading = true;

  constructor(private adminService: AdminService) {}

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
}