import { ShoppingCart as ShoppingCartIcon } from '@mui/icons-material'
import { Fab, Badge } from '@mui/material'
import React from 'react'
import { ShoppingCart, ShoppingCartItem } from '../../global/models/shoppingCart'
import { observer } from 'mobx-react-lite'
import { useStore } from '../../global/stores/store'
import { Link } from 'react-router-dom'

function ShoppingCartBadge() {
  const {shoppingCartStore} = useStore()
  

  return (
    <Link style={{ position: 'fixed', right: 20, bottom: 20 }} replace to={'/koszyk'}>
      <Fab color="primary" aria-label="add to shopping cart">
        <Badge badgeContent={shoppingCartStore.shoppingCart && shoppingCartStore.shoppingCart.items.length} color="secondary">
          <ShoppingCartIcon />
        </Badge>
      </Fab>
    </Link>
  )
}

export default observer(ShoppingCartBadge)