import { MedicalStaffType } from "../../../config/enums/medical-staff-types.enum";
import { Role } from "../../../config/enums/role.enum";

export interface MedicalStaffServiceModel{
    id: number;
    isActive: boolean;
    name: string;
    description: string;
    cost: number;
    staffId: number;
}