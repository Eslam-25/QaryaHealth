import { MedicalStaffType } from "../../../config/enums/medical-staff-types.enum";
import { Role } from "../../../config/enums/role.enum";
import { MedicalStaffServiceModel } from "./medical-staff-service.model";

export interface MedicalStaffModel{
    id: number;
    isActive: boolean;
    name: string;
    address: string;
    phoneNumber: string;
    email: string;
    password: string;

    type: MedicalStaffType;
    bio: string;
    workingDescription: string;
    imagePath: string;
    readyToWork: boolean;

    role: Role;
    userId: number;
    // imagePath: string;

    services: MedicalStaffServiceModel[];
}