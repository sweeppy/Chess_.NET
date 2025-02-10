import axios from "axios";

export const isUserExistsRequestAsync = async (email: string) => {
  try {
    const response = await axios.post(
      "http://localhost:5096/api/Account/IsUserExists",
      email,
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    return response.data;
  } catch (error: any) {
    throw new Error(error.message);
  }
};
