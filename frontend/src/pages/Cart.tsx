import React, { useEffect, useState } from "react";
import NavBar from "../micro-components/navbar.tsx";
import { useColorMode } from "../components/ui/color-mode";
import { useSelector } from "react-redux";
import { get, post } from "../utils/httpRequests.ts";
import { Flex, Heading, Text } from "@chakra-ui/react";
import { Button } from "../components/ui/button";
import { GATEWAY_PORT } from "../environment-variables.ts";
import { useNavigate } from "react-router-dom";

interface ICart {
  productId: string;
  quantity: number;
}
interface IProduct {
  name: string;
  id: string;
  price: number;
}
const Cart = () => {
  const { toggleColorMode } = useColorMode();
  const token = useSelector((state): any => state.user.token);
  const navigate = useNavigate();
  const [carts, setCarts] = useState<ICart[]>([]);
  const [initialCarts, setInitialCarts] = useState<ICart[]>([]);
  const [products, setProducts] = useState<IProduct[]>([]);
  useEffect(() => {
    toggleColorMode();
    const fetchCart = async () => {
      try {
        const data = await get(GATEWAY_PORT, "carts/all", token);
        if (Array.isArray(data)) {
          setCarts(data);
          setInitialCarts(data);
        }
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    };

    fetchCart();
  }, []);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        let model: { id: string; test: boolean }[] = [];
        for (let i = 0; i < carts.length; i++) {
          model.push({
            id: carts[i].productId,
            test: true,
          });
        }
        const data1 = await post(
          GATEWAY_PORT,
          "products/getCart",
          model,
          token
        );
        if (Array.isArray(data1)) {
          setProducts(data1);
        }
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    };
    fetchProducts();
  }, [initialCarts]);

  const getQuantity = (id) => {
    return carts.find((c) => c.productId === id).quantity;
  };

  const isQuantityEmpty = (id) => {
    return carts.find((c) => c.productId === id).quantity === 0;
  };

  const removeProductFromCart = async (id) => {
    const model = {
      productId: id,
      productQuantity: 0,
    };

    await post(GATEWAY_PORT, "carts/remove", model, token, false);
  };

  const addProductToCart = async (id) => {
    const model = {
      productId: id,
      productQuantity: 0,
    };

    await post(GATEWAY_PORT, "carts/add", model, token, false);
  };

  const addOrSubside = async (id, sum) => {
    if (getQuantity(id) === 0) {
      return;
    }
    if (sum > 0) {
      await addProductToCart(id);
    } else {
      await removeProductFromCart(id);
    }
    setCarts((prevCarts) =>
      prevCarts.map((c) =>
        c.productId === id
          ? { ...c, quantity: Math.max(0, c.quantity + sum) }
          : c
      )
    );
  };

  const addOrder = async () => {
    let model: { productId: string; price: number; quantity: number }[] = [];

    for (let i = 0; i < products.length; i++) {
      model.push({
        productId: products[i].id,
        price: products[i].price,
        quantity: getQuantity(products[i].id),
      });
    }

    const resp = await post(GATEWAY_PORT, "orders/add", model, token, false);

    if (resp !== 0) navigate("/home");
  };
  return (
    <React.Fragment>
      <NavBar />
      <Flex justify="center" mt="10">
        <Flex width="60%" direction="column" gap="5">
          {products?.map((p) =>
            getQuantity(p.id) === 0 ? (
              <></>
            ) : (
              <Flex
                key={p.id}
                p="5"
                shadow="lg"
                rounded="2xl"
                direction="column"
              >
                <Flex justify="space-between" direction="row">
                  <Flex direction="column">
                    <Heading>{p.name}</Heading>
                    <Flex align="center" gap="2" mt="2">
                      <span class="material-symbols-outlined">payments</span>
                      <Text>{p.price} EUR</Text>
                    </Flex>
                  </Flex>
                  <Flex align="center" gap="3">
                    <Button
                      colorPalette="gary"
                      variant="outline"
                      size="xs"
                      rounded="full"
                      onClick={() => addOrSubside(p.id, -1)}
                    >
                      -
                    </Button>
                    <Text>{getQuantity(p.id)}</Text>
                    <Button
                      colorPalette="gary"
                      variant="outline"
                      size="xs"
                      rounded="full"
                      onClick={() => addOrSubside(p.id, 1)}
                    >
                      +
                    </Button>
                  </Flex>
                </Flex>
              </Flex>
            )
          )}
          <Button colorPalette="blue" rounded="2xl" onClick={() => addOrder()}>
            Order
          </Button>
        </Flex>
      </Flex>
    </React.Fragment>
  );
};

export default Cart;
