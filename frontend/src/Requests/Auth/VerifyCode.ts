import axios from "axios";

export const VerifyCodeAsync = async (email: string, code: string) => {
  try {
    const response = await axios.put(
      "http://localhost:5096/api/Account/VerifyCode",
      {
        email,
        code,
      },
      {
        headers: { "Content-Type": "application/json" },
      }
    );
    console.log(response);
    console.log(response.data);
    if (response.data.jwtToken) {
      localStorage.setItem("jwtToken", response.data.jwtToken);
    }
  } catch (error) {
    console.error(error);
    throw new Error(
      "An error occurred while verifying your account. Please try again or contact support."
    );
  }
};
