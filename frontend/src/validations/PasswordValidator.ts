import { BaseResponse } from "../models/BaseResponse";

export const validatePassword = (password: string): BaseResponse => {
  // Minimum password length
  if (password.length < 8) {
    return {
      isSuccess: false,
      message: "Password must be at least 8 characters long.",
    };
  }

  // Check for at least one digit
  if (!/\d/.test(password)) {
    return {
      isSuccess: false,
      message: "Password must contain at least one digit.",
    };
  }

  // Check for at least one uppercase letter
  if (!/[A-Z]/.test(password)) {
    return {
      isSuccess: false,
      message: "Password must contain at least one uppercase letter.",
    };
  }

  // Check for at least one lowercase letter
  if (!/[a-z]/.test(password)) {
    return {
      isSuccess: false,
      message: "Password must contain at least one lowercase letter.",
    };
  }

  // Check for at least one special character
  if (!/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(password)) {
    return {
      isSuccess: false,
      message: "Password must contain at least one special character.",
    };
  }

  // If all checks pass
  return { isSuccess: true, message: "Password is valid." };
};
