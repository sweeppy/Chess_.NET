import axios from "axios";

export const VerifyCodeAsync = async (email: string, code: string) => {
  try {
    await axios.put(
      "http://localhost:5096/api/Account/VerifyCode",
      {
        email,
        code,
      },
      {
        headers: { "Content-Type": "application/json" },
      }
    );
  } catch (error) {
    console.error(error);
    throw new Error(
      "An error occurred while verifying your account. Please try again or contact support."
    );
  }
};
