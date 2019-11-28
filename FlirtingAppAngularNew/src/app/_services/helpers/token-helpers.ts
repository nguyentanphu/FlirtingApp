export function accessTokenGetter() {
  return localStorage.getItem('accessToken');
}

export function refreshTokenGetter() {
  return localStorage.getItem('refreshToken');
}
