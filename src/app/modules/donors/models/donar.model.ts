import { BloodType } from "../../../config/enums/blood-types.enum";
import { Gender } from "../../../config/enums/gender.enum";
import { Role } from "../../../config/enums/role.enum";

export interface DonarModel{
    id: number;
    name: string;
    birthDate: Date;
    bloodType: BloodType;
    address: string;
    phoneNumber: string;
    email: string;
    password: string;
    lastDonationDate: Date;
    gender: Gender;
    role: Role;
    readyToDonor: boolean;
}