import axios from "axios";

export const isUserExistsAndEmailConfirmedAsync = async (email: string) => {
  try {
    const response = await axios.post(
      "http://localhost:5096/api/Account/IsUserExistsAndEmailConfirmed",
      email,
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    return response.data;
  } catch (error: any) {
    console.error(error);
    throw new Error(
      "En error occurred while checking your account. Please try again or contact support."
    );
  }
};
