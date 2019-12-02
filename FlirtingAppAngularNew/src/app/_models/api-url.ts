export const APIURL = {
  auth: {
    login: 'auth/login',
    logout: 'auth/logout',
    exchangeTokens: 'auth/exchangeTokens'
  },
  users: {
    get: 'users',
    create: 'users',
    getUserDetail: (userId: string) => `users/${userId}`,
    updateAdditionalDetail: 'users'
  }
};
