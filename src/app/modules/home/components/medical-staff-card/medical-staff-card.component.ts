import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MedicalStaffType } from '../../../../config/enums/medical-staff-types.enum';
import { MedicalStaffTypeModel } from '../../../../config/models/medical-staff-types.model';

@Component({
  selector: 'medical-staff-card',
  templateUrl: './medical-staff-card.component.html',
  styleUrls: ['./medical-staff-card.component.css']
})
export class MedicalStaffCardComponent implements OnInit {

  @Input() title: string = "";
  @Input() staffTypes: MedicalStaffTypeModel[] = [];

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  onTypeClick(type: MedicalStaffType){
    this.router.navigateByUrl(`/medical-saff?type=${type}`);
  }
}
