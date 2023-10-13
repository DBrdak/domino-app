import React, {Component, useEffect, useState} from "react";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../global/stores/store";
import {Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography} from "@mui/material";
import ProductListItem from "../../onlineShop/productList/ProductListItem";
import {PriceList} from "../../../global/models/priceList";
import PriceListList from "./PriceListList";
import PriceListCreateSection from "../priceListCreation/PriceListCreateSection";

function PriceListsSection() {

    return (
        <>
            <PriceListCreateSection />
            <PriceListList />
        </>
    )
}

export default PriceListsSection