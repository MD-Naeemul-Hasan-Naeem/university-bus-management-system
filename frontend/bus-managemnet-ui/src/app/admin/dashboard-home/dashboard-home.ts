import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../core/services/admin';
import { DashboardStats } from '../../core/models/dashboard-stats.model';

@Component({
  selector: 'app-dashboard-home',
  templateUrl: './dashboard-home.html'
})
export class DashboardHomeComponent implements OnInit {

  stats: DashboardStats | null = null;
  loading: boolean = true;
  errorMessage: string = '';

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.loadDashboard();
  }

  loadDashboard(): void {
    this.loading = true;

    this.adminService.getDashboardStats().subscribe({
      next: (response: DashboardStats) => {
        this.stats = response;
        this.loading = false;
      },
      error: (error) => {
        console.error('Dashboard load error:', error);
        this.errorMessage = 'Failed to load dashboard data';
        this.loading = false;
      }
    });
  }
}