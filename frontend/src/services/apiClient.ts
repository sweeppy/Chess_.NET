import axios, {
  AxiosInstance,
  AxiosResponse,
  AxiosError,
  InternalAxiosRequestConfig,
} from 'axios';

const baseClient = axios.create({
  withCredentials: true,
});

const requestInterceptor = (
  config: InternalAxiosRequestConfig
): InternalAxiosRequestConfig => {
  const token = localStorage.getItem('jwtToken');
  if (token) {
    config.headers = config.headers || {};
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
};

const responseSuccessInterceptor = (response: AxiosResponse): AxiosResponse =>
  response;

const responseErrorInterceptor = async (error: AxiosError): Promise<never> => {
  const originalRequest = error.config as InternalAxiosRequestConfig & {
    _retry?: boolean;
  };

  if (
    error.response?.status === 401 &&
    originalRequest &&
    !originalRequest._retry
  ) {
    originalRequest._retry = true;

    try {
      const refreshResponse = await axios.post<{ accessToken: string }>(
        'http://localhost:5096/api/Account/RefreshToken',
        {},
        { withCredentials: true }
      );

      const newAccessToken = refreshResponse.data.accessToken;
      localStorage.setItem('jwtToken', newAccessToken);

      originalRequest.headers = originalRequest.headers || {};
      originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
      return baseClient(originalRequest);
    } catch (refreshError) {
      localStorage.removeItem('jwtToken');
      window.location.href = '/login';
      return Promise.reject(refreshError);
    }
  }

  return Promise.reject(error);
};

baseClient.interceptors.request.use(requestInterceptor);
baseClient.interceptors.response.use(
  responseSuccessInterceptor,
  responseErrorInterceptor
);

export const accountClient: AxiosInstance = axios.create({
  baseURL: 'http://localhost:5096/api',
  withCredentials: true,
});

export const chessClient: AxiosInstance = axios.create({
  baseURL: 'http://localhost:5011/api',
  withCredentials: true,
});

const setupClientInterceptors = (client: AxiosInstance): AxiosInstance => {
  client.interceptors.request.use(requestInterceptor);
  client.interceptors.response.use(
    responseSuccessInterceptor,
    responseErrorInterceptor
  );
  return client;
};

setupClientInterceptors(accountClient);
setupClientInterceptors(chessClient);

export default {
  account: accountClient,
  chess: chessClient,
};
