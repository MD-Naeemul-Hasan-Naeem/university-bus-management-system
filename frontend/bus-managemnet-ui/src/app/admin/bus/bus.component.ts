import { Component, OnInit } from '@angular/core';
import { BusService, Bus } from '././../../core/services/bus';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-bus',
  templateUrl: './bus.component.html',
  standalone: true,
  imports: [FormsModule, HttpClientModule, CommonModule]
})
export class BusComponent implements OnInit {

  buses: Bus[] = [];
  selectedBus: Bus = {};
  isEdit = false;

  constructor(private busService: BusService) {}

  ngOnInit(): void {
    this.loadBuses();
  }

  loadBuses() {
    this.busService.getAll().subscribe(res => {
      this.buses = res;
    });
  }

  openCreate() {
    this.selectedBus = {};
    this.isEdit = false;
  }

  openEdit(bus: Bus) {
    this.selectedBus = { ...bus };
    this.isEdit = true;
  }

  save() {
    if (this.isEdit) {
      this.busService.update(this.selectedBus).subscribe(() => {
        this.loadBuses();
      });
    } else {
      this.busService.create(this.selectedBus).subscribe(() => {
        this.loadBuses();
      });
    }
  }

  delete(id: number) {
    if (confirm('Are you sure?')) {
      this.busService.delete(id).subscribe(() => {
        this.loadBuses();
      });
    }
  }

  toggleStatus(bus: Bus) {
    const newStatus = bus.status === 'Active' ? 'Inactive' : 'Active';
    this.busService.changeStatus(bus.id!, newStatus).subscribe(() => {
      this.loadBuses();
    });
  }
}