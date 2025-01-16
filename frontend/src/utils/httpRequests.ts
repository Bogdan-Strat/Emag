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
      "Authorization": `Bearer ${token}`,
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

export const post = async (
  port,
  path,
  model,
  token,
  hasResult = true,
) => {
  const response = await fetch(`http://localhost:${port}/${path}`, {
    method: "POST",
    headers: {
      "Authorization": `Bearer ${token}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(model),
  });

  if (!response.ok) return 0;

  if (hasResult) {
    const data = await response.json();

    return data;
  }
};

export const get = async (port, path, token ) => {
  const response = await fetch(`http://localhost:${port}/${path}`, {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
      "Content-Type": "application/json",
    }
  });

  if (!response.ok) return 0;

    const data = await response.json();
    return data;
}
