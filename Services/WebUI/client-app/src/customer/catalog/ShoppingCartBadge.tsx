import { ShoppingCart as ShoppingCartIcon } from '@mui/icons-material'
import { Fab, Badge, CircularProgress } from '@mui/material'
import React from 'react'
import { ShoppingCart, ShoppingCartItem } from '../../global/models/shoppingCart'
import { observer } from 'mobx-react-lite'
import { useStore } from '../../global/stores/store'
import { Link } from 'react-router-dom'

function ShoppingCartBadge() {
  const {shoppingCartStore} = useStore()
  

  return (
    shoppingCartStore.loading ?
    <Fab style={{ position: 'fixed', right: 20, bottom: 20 }} color="primary" aria-label="add to shopping cart">
      <CircularProgress color='secondary' size={25} />
    </Fab>
    :
    shoppingCartStore.shoppingCart ?
    <Link style={{ position: 'fixed', right: 20, bottom: 20 }} replace to={'/koszyk'}>
      <Fab color="primary" aria-label="add to shopping cart">
        <Badge badgeContent={shoppingCartStore.shoppingCart && shoppingCartStore.shoppingCart.items.length} color="secondary">
          <ShoppingCartIcon />
        </Badge>
      </Fab>
    </Link>
    :
    <Fab style={{ position: 'fixed', right: 20, bottom: 20 }}  color="primary" aria-label="add to shopping cart" disabled={true}>
      <Badge color="secondary">
        <ShoppingCartIcon />
      </Badge>
    </Fab>
  )
}

export default observer(ShoppingCartBadge)