export const postWithoutAuth = async (
  port,
  path,
  model,
  hasResult = true,
  token = ""
) => {
  let headers;

  if (token !== "") {
    headers = {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    };
  } else {
    headers = {
      "Content-Type": "application/json",
    };
  }

  const response = await fetch(`http://localhost:${port}/${path}`, {
    method: "POST",
    headers: headers,
    body: JSON.stringify(model),
  });

  if (!response.ok) return 0;

  if (hasResult) {
    const data = await response.json();

    return data;
  }
};
