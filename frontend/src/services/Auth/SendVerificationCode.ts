import axios from "axios";

export const SendVerificationCodeAsync = async (email: string) => {
  try {
    await axios.post(
      "http://localhost:5096/api/Account/SendVerificationCode",
      email,
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
  } catch (error: any) {
    console.error(error);
    throw new Error(
      "An error was occurred while trying to send you email. Please try again or contact support."
    );
  }
};
