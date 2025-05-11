import axios from 'axios';
import { accountClient } from '../apiClient';

export const validateToken = async () => {
  try {
    const token = localStorage.getItem('jwtToken');

    if (!token) {
      console.warn('token not found');
      return false;
    }

    const response = await accountClient.get('/Account/ValidateToken');

    return response.data.isValid;
  } catch (error) {
    console.error('Token validation failed:', error);
    localStorage.removeItem('jwtToken');
    return false;
  }
};
