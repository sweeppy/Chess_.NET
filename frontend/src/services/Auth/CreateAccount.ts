import axios from 'axios';

export const createAccountAsync = async (
  username: string,
  password: string,
  confirmPassword: string,
  croppedImage: string
) => {
  try {
    const formData = new FormData();
    formData.append('Username', username);
    formData.append('Password', password);
    formData.append('ConfirmPassword', confirmPassword);

    if (croppedImage.startsWith('data:image')) {
      const blob = await fetch(croppedImage).then((res) => res.blob());
      formData.append('profileImage', blob, 'profile.png');
    } else {
      console.error('Invalid croppedImage format:', croppedImage);
      throw new Error('Invalid image format. Please upload a valid image.');
    }

    await axios.post(
      'http://localhost:5096/api/Account/CreateAccount',
      formData,
      {
        headers: {
          'Content-Type': 'multipart/form-data',
          Authorization: `Bearer ${localStorage.getItem('jwtToken')}`,
        },
      }
    );
  } catch (error: any) {
    console.error(error);
    throw new Error(
      'An error occurred while creating your account. Please try again or contact support.'
    );
  }
};
