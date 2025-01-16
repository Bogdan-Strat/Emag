import NavBar from "../micro-components/navbar.tsx";
import React, { useEffect, useState } from "react";
import { Flex, Input, Heading, Text, Button } from "@chakra-ui/react";
import { useColorMode } from "../components/ui/color-mode";
import { toaster, Toaster } from "../components/ui/toaster";
import { get, post } from "../utils/httpRequests.ts";
import { GATEWAY_PORT, PRODUCT_PORT } from "../environment-variables.ts";
import { useSelector } from "react-redux";

interface IProduct {
  id: string;
  category: string;
  description: string;
  price: number;
  name: string;
}
const Home = () => {
  const { toggleColorMode } = useColorMode();
  const token = useSelector((state): any => state.user.token);
  const [products, setProducts] = useState<IProduct[]>([]);

  useEffect(() => {
    toggleColorMode();
    const fetchProducts = async () => {
      try {
        const data = await get(GATEWAY_PORT, "products/all", token);
        if (Array.isArray(data)) {
          setProducts(data); // Only set products if the response is an array
        } else {
          console.error("Unexpected data format:", data);
        }
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    };
    fetchProducts();
  }, []);

  const addToCart = async (id) => {
    const model = {
      productId: id,
      productQuantity: 0,
    };

    const res = await post(GATEWAY_PORT, "carts/add", model, token, false);
    if (res !== 0) {
      toaster.create({
        title: `Product addeed to cart`,
        type: "success",
      });
    }
  };
  return (
    <React.Fragment>
      <Toaster />
      <NavBar />
      <Flex justify="center" mt="10">
        <Flex width="60%" direction="column" gap="5" mb="10">
          {products?.map((p) => (
            <Flex key={p.id} p="5" shadow="lg" rounded="2xl" direction="column">
              <Heading>{p.name}</Heading>
              <Flex justify="space-between" mt="3">
                <Flex direction="column" gap="2">
                  <Flex align="center" gap="2">
                    <span class="material-symbols-outlined">description</span>
                    <Text>{p.description}</Text>
                  </Flex>
                  <Flex align="center" gap="2">
                    <span class="material-symbols-outlined">category</span>
                    <Text>{p.category}</Text>
                  </Flex>
                  <Flex align="center" gap="2">
                    <span class="material-symbols-outlined">payments</span>
                    <Text>{p.price} RON</Text>
                  </Flex>
                </Flex>
                <Flex direction="column">
                  <Button
                    colorPalette="blue"
                    size="xl"
                    rounded="2xl"
                    onClick={() => addToCart(p.id)}
                  >
                    Add to cart
                  </Button>
                </Flex>
              </Flex>
            </Flex>
          ))}
        </Flex>
      </Flex>
    </React.Fragment>
  );
};

export default Home;
